import { AxiosInstance } from 'axios';
import { RoomType } from '@/booking/RoomType';

export class BookingHttpRepository {
  constructor(private axiosInstance: AxiosInstance) {}

  public pay(amount: number, startDate: string, endDate: string, roomType: RoomType) {
    this.axiosInstance.post('/api/booking/book-room', {
      amount,
      startDate,
      endDate,
      guestId: '123',
      roomType,
    });
  }
}
