import { BookingHttpRepository } from '@/booking/BookingHttpRepository';
import sinon, { SinonStub } from 'sinon';
import { RoomType } from '@/booking/RoomType';
import { Availability } from '@/booking/Availability';

const START_DATE = '2019-09-10';
const END_DATE = '2019-09-11';

interface StubbedAxios {
  post: SinonStub;
  get: SinonStub;
}

describe('Booking HTTP Repository', () => {
  const stubAxios: StubbedAxios = {
    post: sinon.stub(),
    get: sinon.stub(),
  };
  const bookingHttpRepository = new BookingHttpRepository(stubAxios as any);
  const KING_DATES = [
    '2019-09-08',
    '2019-09-10',
    '2019-09-11',
  ];
  const TWIN_DATES = [
    '2019-09-09',
    '2019-09-10',
    '2019-09-12',
  ];

  it('Should pay', () => {
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

  it('Should check availability', async () => {
    stubAxios.get.resolves({
      data: new Availability(KING_DATES, TWIN_DATES),
    });

    const availability = await bookingHttpRepository.availability();

    expect(stubAxios.get.getCall(0).args[0]).toEqual('/api/availability');
    expect(availability.king).toEqual(KING_DATES);
    expect(availability.twin).toEqual(TWIN_DATES);
  });
});
