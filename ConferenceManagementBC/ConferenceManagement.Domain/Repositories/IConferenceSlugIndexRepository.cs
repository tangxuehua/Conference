using System.Threading.Tasks;
using ConferenceManagement.Domain.Models;

namespace ConferenceManagement.Domain.Repositories
{
    public interface IConferenceSlugIndexRepository
    {
        Task Add(ConferenceSlugIndex index);
        Task<ConferenceSlugIndex> FindSlugIndex(string slug);
    }
}
