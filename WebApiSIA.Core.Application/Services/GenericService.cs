using AutoMapper;
using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Application.Interfaces.Services;

namespace WebApiSIA.Core.Application.Services
{
    public class GenericService<TSaveDto, TDto, TEntity> : IGenericService<TSaveDto, TDto, TEntity>
        where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repo;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return default;
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TSaveDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> UpdateAsync(int id, TSaveDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No se encontró {typeof(TEntity).Name} con id {id}");

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity, id);

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return;
            await _repo.DeleteAsync(entity);
        }
    }
}
