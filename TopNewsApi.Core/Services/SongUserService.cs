using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Entities.Specifications;
using TopNewsApi.Core.Entities.User;
using TopNewsApi.Core.Interfaces;

namespace TopNewsApi.Core.Services
{
    public class SongUserService: ISongUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SongUser> _songService;
        public SongUserService(IMapper mapper, IRepository<SongUser> songService)
        {
            this._songService = songService;
            this._mapper = mapper;
        }
        public async Task Create(SongUserDto model)
        {
            await _songService.Insert(_mapper.Map<SongUser>(model));
            await _songService.Save();
        }

        public async Task Delete(int id)
        {
            SongUserDto? model = await Get(id);
            if (model == null) return;
            await _songService.Delete(id);
            await _songService.Save();
        }

        public async Task Delete(SongUserDto model)
        {
            await _songService.Delete(model);
            await _songService.Save();
        }

        public async Task<SongUserDto?> Get(int id)
        {
            if (id < 0) return null;
            SongUser? category = await _songService.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<SongUserDto?>(category);
        }

        public async Task<List<SongUserDto>> GetAll(string userId)
        {
            try
            {
                var result = await _songService.GetListBySpec(new SongUserSpecification.GetByUserId(userId));
                if (result != null)
                {
                    return _mapper.Map<List<SongUserDto>>(result);
                }
                //return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting all songuser: ", ex.Message);
            }
            return null;
        }

        public async Task<List<SongUserDto>> GetAll()
        {
            var result = await _songService.GetAll();
            return _mapper.Map<List<SongUserDto>>(result);
        }

        //public async Task<ServiceResponse> GetByName(string name)
        //{
        //    var result = await songService.GetItemBySpec(new GroupSpecification.GetByName(name));
        //    if (result != null)
        //    {
        //        return new ServiceResponse(false, "Group exists.");
        //    }
        //    var category = _mapper.Map<GroupDTO>(result);
        //    return new ServiceResponse(true, "Group successfully loaded.", payload: category);
        //}

        public async Task Update(SongUserDto model)
        {
            await _songService.Update(_mapper.Map<SongUser>(model));
            await _songService.Save();
        }
    }
}
