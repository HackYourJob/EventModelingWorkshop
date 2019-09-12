using System.Threading.Tasks;

namespace Domain
{
	public interface IRoomRepository
	{
		Task<RoomId[]> GetNotCheckedRoomIds();
		Task<RoomId[]> GetCheckedRoomIds();
		Task<RoomId[]> GetDirtyRoomIds();
	}
}