import { Component, Inject, Vue } from 'vue-property-decorator';
import { BookingHttpRepository } from '@/booking/BookingHttpRepository';
import { RoomType } from '@/booking/RoomType';

@Component
export default class BookingComponent extends Vue {
  public amount = '300';
  public startDate = '';
  public endDate = '';
  public roomType = '';

  @Inject()
  private bookingRepository!: () => BookingHttpRepository;

  public pay(): void {
    const roomType = this.roomType === 'twin' ? RoomType.TWIN : RoomType.KING;
    this.bookingRepository().pay(+this.amount, this.startDate, this.endDate, roomType);
  }
}
