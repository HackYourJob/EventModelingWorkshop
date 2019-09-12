import { Component, Inject, Vue } from 'vue-property-decorator';
import { BookingHttpRepository } from '@/booking/BookingHttpRepository';
import { RoomType } from '@/booking/RoomType';
import { Availability } from '@/booking/Availability';

const KING = 'king';
const TWIN = 'twin';

@Component
export default class BookingComponent extends Vue {
  public amount = '300';
  public startDate = '';
  public endDate = '';
  public availability: string[] = [];
  private roomType?: RoomType;
  private repoAvailability!: Availability;

  @Inject()
  private bookingRepository!: () => BookingHttpRepository;

  public async created(): Promise<void> {
    this.repoAvailability = await this.bookingRepository().availability();
    this.updateAvailability();
  }

  public beKing(): void {
    this.roomType = RoomType.KING;
    this.updateAvailability();
  }

  public beTwin(): void {
    this.roomType = RoomType.TWIN;
    this.updateAvailability();
  }

  public pay(): void {
    this.bookingRepository().pay(
      +this.amount,
      this.startDate,
      this.endDate,
      this.roomType!,
    );
  }

  public updateAvailability(): void {
    if (this.roomType === TWIN) {
      this.availability = this.repoAvailability.twin;
      return;
    }
    if (this.roomType === KING) {
      this.availability = this.repoAvailability.king;
      return;
    }
    this.availability = this.repoAvailability.both;
    return;
  }
}
