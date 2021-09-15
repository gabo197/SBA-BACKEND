using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using SBA_BACKEND.Domain.Models;
 using SBA_BACKEND.Domain.Services.Communications;
 using SBA_BACKEND.Domain.Services;
 using SBA_BACKEND.Domain.Persistence.Repositories;
 
 
 namespace SBA_BACKEND.Services
 {
 	public class TechnicalService : ITechnicalService
 	{
 		private readonly ITechnicalRepository _technicalRepository;
 		private IUnitOfWork _unitOfWork;
 		public TechnicalService(ITechnicalRepository object1, IUnitOfWork object2)
 		{
 			this._technicalRepository = object1;
 			this._unitOfWork = object2;
 		}
 
 		public async Task<TechnicalResponse> DeleteAsync(int id)
 		{
 			var existingTechnical = await _technicalRepository.FindById(id);
 
 			if (existingTechnical == null)
 				return new TechnicalResponse("Technical not found");
 
 			try
 			{
 				_technicalRepository.Remove(existingTechnical);
 				await _unitOfWork.CompleteAsync();
 
 				return new TechnicalResponse(existingTechnical);
 			}
 			catch (Exception ex)
 			{
 				return new TechnicalResponse($"An error ocurred while deleting the technical: {ex.Message}");
 			}
 		}
 
 		public async Task<TechnicalResponse> GetByIdAsync(int id)
 		{
 			var existingTechnical = await _technicalRepository.FindById(id);
 
 			if (existingTechnical == null)
 				return new TechnicalResponse("Technical not found");
 			return new TechnicalResponse(existingTechnical);
 		}
 
 		public async Task<IEnumerable<Technical>> ListAsync()
 		{
 			return await _technicalRepository.ListAsync();
 		}
 
 		public async Task<TechnicalResponse> SaveAsync(Technical technical)
 		{
 			try
 			{
 				await _technicalRepository.AddAsync(technical);
 				await _unitOfWork.CompleteAsync();
 				return new TechnicalResponse(technical);
 			}
 			catch (Exception e)
 			{
 				return new TechnicalResponse($"An error ocurred while saving the technical: {e.Message}");
 			}
 		}
 		public async Task<TechnicalResponse> UpdateAsync(int id, Technical technical)
 		{
 			var existingTechnical = await _technicalRepository.FindById(id);
 
 			if (existingTechnical == null)
 				return new TechnicalResponse("Technical not found");

            //falta llenar los updates

            try
            {
 				_technicalRepository.Update(existingTechnical);
 				await _unitOfWork.CompleteAsync();
 				return new TechnicalResponse(existingTechnical);
 			}
 			catch (Exception ex)
 			{
 				return new TechnicalResponse($"An error ocurred while updating the technical: {ex.Message}");
 			}
 		}
 	}
 }
