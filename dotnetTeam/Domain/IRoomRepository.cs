using System.Threading.Tasks;

namespace Domain
{
	public interface IRoomRepository
	{
		Task<RoomId[]> GetNotCheckedRoomIds();
		Task<RoomId[]> GetCheckedInRoomIds();
		Task<RoomId[]> GetDirtyRoomIds();
	}
}