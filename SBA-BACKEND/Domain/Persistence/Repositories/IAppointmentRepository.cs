﻿using SBA_BACKEND.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Persistence.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> ListAsync();
        Task<IEnumerable<Appointment>> ListByCustomerIdAsync(int id);
        Task<IEnumerable<Appointment>> ListByTechnicianIdAsync(int id);
        Task AddAsync(Appointment appointment);
        Task<Appointment> FindById(int id);
        void Update(Appointment appointment);
        void Remove(Appointment appointment);
    }
}
