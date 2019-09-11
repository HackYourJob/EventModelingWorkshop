import { BookingHttpRepository } from '@/booking/BookingHttpRepository';
import sinon, { SinonStub } from 'sinon';
import { RoomType } from '@/booking/RoomType';

const START_DATE = '2019-09-10';
const END_DATE = '2019-09-11';

interface StubbedAxios {
  post: SinonStub;
}

describe('Booking HTTP Repository', () => {
  it('Should pay', () => {
    const stubAxios: StubbedAxios = {
      post: sinon.stub(),
    };
    const bookingHttpRepository = new BookingHttpRepository(stubAxios as any);
    bookingHttpRepository.pay(300, START_DATE, END_DATE, RoomType.TWIN);

    expect(stubAxios.post.getCall(0).args[0]).toEqual('/api/booking/book-room');
    expect(stubAxios.post.getCall(0).args[1]).toEqual({
      amount: 300,
      guestId: '123',
      roomType: RoomType.TWIN,
      startDate: START_DATE,
      endDate: END_DATE,
    });
  });
});
