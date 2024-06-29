using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.User;
using TopNewsApi.Core.Services;

namespace TopNewsApi.Core.Interfaces
{
    public interface ISongService
    {
        Task Create(SongsDto songs);
        Task Delete(SongsDto songs);
        Task Delete(int id);
        Task Update(SongsDto songs);
        Task<SongsDto?> Get(int songid);
        Task<List<SongsDto>>GetAll();
        Task<ServiceResponse> GetByName(string title);
        Task<ServiceResponse> GetById(int songId);
    }
}
