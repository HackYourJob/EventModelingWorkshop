import { AxiosInstance } from 'axios';
import { RoomType } from '@/booking/RoomType';
import { Availability } from '@/booking/Availability';

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

  public async availability(): Promise<Availability> {
    const availabilityHTTP = await this.axiosInstance.get('/api/availability');
    const {king, twin} = availabilityHTTP.data;
    return new Availability(king, twin);
  }
}
