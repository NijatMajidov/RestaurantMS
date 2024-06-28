using RMS.Business.Exceptions;
using RMS.Business.Services.Abstracts;
using RMS.Core.Entities;
using RMS.Data.Repositories.Abstractions;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace RMS.Business.Services.Concretes
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public async Task Create(Restaurant CreateDTO)
        {
            if (CreateDTO == null)
                throw new EntityNullReferenceException("", "Data null reference");
            if (!Regex.IsMatch(CreateDTO.Phone, @"^\d{10}$"))
                throw new PhoneFormatException("Phone", "Phone by ten digits");
            _restaurantRepository.Add(CreateDTO);
            await _restaurantRepository.CommitAsync();
        }

        public async Task DeleteTable(int id)
        {
            var restaurant = await _restaurantRepository.GetAsync(x=> x.Id == id);
            if (restaurant == null)
                throw new EntityNotFoundException("", "data not found");
            _restaurantRepository.Delete(restaurant);
            await _restaurantRepository.CommitAsync();
        }

        public async Task<List<Restaurant>> GetAllTables(Expression<Func<Restaurant, bool>>? func = null, Expression<Func<Restaurant, object>>? orderBy = null, bool isOrderByDesting = false, params string[]? includes)
        {
            var queryable = await _restaurantRepository.GetAllAsync(func, orderBy, isOrderByDesting, includes);
            return queryable.ToList();
        }

        public async Task<Restaurant> GetTable(Func<Restaurant, bool>? func = null)
        {
            var entity = _restaurantRepository.Get(func);
            if (entity == null) throw new EntityNotFoundException("", "Table not Found");

            return entity;
        }

        public async Task SoftDeleteTable(int id)
        {
            var entity = await _restaurantRepository.GetAsync(x => x.Id == id);
            if (entity == null) throw new EntityNotFoundException("", "data not found");
            _restaurantRepository.SoftDelete(entity);
            await _restaurantRepository.CommitAsync();
        }

        public async Task Update(int id, Restaurant UpdateDTO)
        {
            var oldData = await _restaurantRepository.GetAsync(x => x.Id == id);
            if (UpdateDTO == null)
                throw new EntityNullReferenceException("", "Data null reference");
            oldData.Street = UpdateDTO.Street;
            oldData.City = UpdateDTO.City;
            oldData.Country = UpdateDTO.Country;
            oldData.District= UpdateDTO.District;
            oldData.Email   = UpdateDTO.Email;
            if (!Regex.IsMatch(UpdateDTO.Phone, @"^\d{10}$"))
                throw new PhoneFormatException("Phone", "Phone by ten digits");
            oldData.Phone = UpdateDTO.Phone;
            oldData.BuildNum   = UpdateDTO.BuildNum;
        }
    }
}
