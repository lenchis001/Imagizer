using AutoMapper;
using Imagizer.BLL.Models;
using Imagizer.DAL.UnitOfWork.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imagizer.BLL.Services
{
	public class CrudService<T, ADD_TYPE, R, RT> : ICrudService<T, ADD_TYPE> where RT : DAL.Models.IdAware
														 where R : IRepository<RT>
	{
		protected readonly IMapper _mapper;
		protected readonly R _repository;

		public CrudService(
			IMapper mapper,
			R repository)
		{
			_mapper = mapper;
			_repository = repository;
		}

		public async Task<DefaultDataFetchResult<T>> AddAsync(ADD_TYPE entity)
		{
			var dalEntity = _mapper.Map<RT>(entity);

			var result = new DefaultDataFetchResult<T>
			{
				Error = ErrorCode.UNKNOWN
			};

			try
			{
				var newEntity = await _repository.AddAsync(dalEntity);

				result.Data = _mapper.Map<T>(newEntity);
				result.Error = ErrorCode.OK;
			}
			catch (System.Exception) { }

			return result;
		}

		public async Task<DefaultFetchResult> DeleteAsync(int id)
		{
			var result = new DefaultFetchResult
			{
				Error = ErrorCode.UNKNOWN
			};

			try
			{
				await _repository.DeteleAsync(id);
				result.Error = ErrorCode.OK;
			}
			catch (System.Exception) { }

			return result;
		}

		public async Task<DefaultDataFetchResult<ICollection<T>>> GetAllAsync()
		{
			var result = new DefaultDataFetchResult<ICollection<T>>
			{
				Error = ErrorCode.UNKNOWN
			};

			try
			{
				var dalEntities = await _repository.GetAllAsync();

				result.Data = _mapper.Map<ICollection<T>>(dalEntities);
				result.Error = ErrorCode.OK;
			}
			catch (System.Exception) { }

			return result;
		}

		public async Task<DefaultDataFetchResult<T>> GetByIdAsync(int id)
		{
			var result = new DefaultDataFetchResult<T>
			{
				Error = ErrorCode.UNKNOWN
			};

			try
			{
				var dalEntity = await _repository.GetByIdAsync(id);
				result.Data = _mapper.Map<T>(dalEntity);
				result.Error = ErrorCode.OK;
			}
			catch (System.Exception) { }

			return result;
		}

		public async Task<DefaultDataFetchResult<T>> UpdateAsync(T entity)
		{
			var dalEntity = _mapper.Map<RT>(entity);

			var result = new DefaultDataFetchResult<T>
			{
				Error = ErrorCode.UNKNOWN
			};

			try
			{
				var updatedEntity = await _repository.UpdateAsync(dalEntity);

				result.Data = _mapper.Map<T>(updatedEntity);
				result.Error = ErrorCode.OK;
			}
			catch (System.Exception) { }

			return result;
		}
	}
}
