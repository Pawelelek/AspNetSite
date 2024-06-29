using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Entities.Specifications;
using TopNewsApi.Core.Entities.Tokens;
using TopNewsApi.Core.Entities.User;
using TopNewsApi.Core.Interfaces;

namespace TopNewsApi.Core.Services
{
    public class SongService: ISongService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Song> _songService;
        public SongService(IMapper mapper, IRepository<Song> songService)
        {
            this._songService = songService;
            this._mapper = mapper;
        }
        public async Task Create(SongsDto model)
        {
            await _songService.Insert(_mapper.Map<Song>(model));
            await _songService.Save();
        }

        public async Task<ServiceResponse> GetByName(string title)
        {
            var result = await _songService.GetItemBySpec(new SongSpecification.GetBySongName(title));
            if (result == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "false"
                };
            }
            var category = result.Id;
            return new ServiceResponse
            {
                Success = true,
                Message = "Song successfully loaded.",
                Payload = category
            };
        }

        public async Task<ServiceResponse> GetById(int songId)
        {
            var result = await _songService.GetItemBySpec(new SongSpecification.GetBySongId(songId));
            if (result == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "false."
                };
            }
            return new ServiceResponse
            {
                Success = true,
                Message = "song successfully loaded.",
                Payload = result
            };
        }

        public async Task Delete(int id)
        {
            SongsDto? model = await Get(id);
            if (model == null) return;
            await _songService.Delete(id);
            await _songService.Save();
        }

        public async Task Delete(SongsDto model)
        {
            await _songService.Delete(model);
            await _songService.Save();
        }

        public async Task<SongsDto?> Get(int id)
        {
            if (id < 0) return null;
            Song? category = await _songService.GetByID(id);
            if (category == null) return null;
            return _mapper.Map<SongsDto?>(category);
        }

        public async Task<List<SongsDto>> GetAll()
        {
            var result = await _songService.GetAll();
            return _mapper.Map<List<SongsDto>>(result);
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

        public async Task Update(SongsDto model)
        {
            await _songService.Update(_mapper.Map<Song>(model));
            await _songService.Save();
        }
    }
}
