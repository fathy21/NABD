using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class ToolRepository : IToolRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ToolRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tool>> GetAllAsync()
        {
            return await _dbContext.Tools.Include(t => t.Patient).ToListAsync() ?? Enumerable.Empty<Tool>();
        }

        public async Task<Tool?> GetByIdAsync(int id)
        {
            return await _dbContext.Tools.Include(t => t.Patient)
                                         .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tool> CreateAsync(Tool tool)
        {
            _dbContext.Tools.Add(tool);
            await _dbContext.SaveChangesAsync();
            return tool;
        }

        public async Task<Tool?> UpdateAsync(int Id, Tool tool)
        {
            var existTool = await _dbContext.Tools.Include(t => t.MQTTMessages)
                                                  .Include(t => t.Emergencies)
                                                  .Include(t => t.Notifications)
                                                  .FirstOrDefaultAsync(x => x.Id == Id);
            if (existTool == null)
            {
                return null;
            }

            existTool.QrCode = tool.QrCode;
            existTool.PatientId = tool.PatientId;
            if (tool.MQTTMessages?.Any() == true)
                existTool.MQTTMessages = tool.MQTTMessages;

            if (tool.Emergencies?.Any() == true)
                existTool.Emergencies = tool.Emergencies;

            if (tool.Notifications?.Any() == true)
                existTool.Notifications = tool.Notifications;

            await _dbContext.SaveChangesAsync();
            return existTool;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tool = await _dbContext.Tools.FindAsync(id);
            if (tool == null) return false;

            _dbContext.Tools.Remove(tool);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
