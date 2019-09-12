import { shallowMount, Wrapper } from '@vue/test-utils';
import Booking from '@/booking/Booking.vue';
import BookingComponent from '@/booking/Booking.component.ts';
import { BookingHttpRepository } from '@/booking/BookingHttpRepository';
import { SinonStubbedInstance } from 'sinon';
import sinon from 'sinon';
import { Availability } from '@/booking/Availability';

const AVAILABILITY = new Availability([
  '2019-09-08',
  '2019-09-10',
  '2019-09-11',
], [
  '2019-09-09',
  '2019-09-10',
  '2019-09-12',
]);

let wrapper: Wrapper<BookingComponent>;
let bookingComponent: BookingComponent;
let stubbedBookingHttpRepository: SinonStubbedInstance<BookingHttpRepository>;

describe('Booking', () => {
  beforeEach(async () => {
    stubbedBookingHttpRepository = sinon.createStubInstance(BookingHttpRepository);
    stubbedBookingHttpRepository.availability.resolves(AVAILABILITY);
    wrapper = await shallowMount<BookingComponent>(Booking, {
      provide: {
        bookingRepository: () => stubbedBookingHttpRepository,
      },
    });
    bookingComponent = wrapper.vm;
  });

  it('Should be a vue instance', () => {
    expect(wrapper.isVueInstance()).toBeTruthy();
  });

  it('Should pay', () => {
    bookingComponent.amount = '300';
    bookingComponent.startDate = '2019-09-10';
    bookingComponent.endDate = '2019-09-11';
    bookingComponent.beKing();

    bookingComponent.pay();

    expect(stubbedBookingHttpRepository.pay.getCall(0).args).toEqual([
      300,
      '2019-09-10',
      '2019-09-11',
      'king',
    ]);
  });

  it('Should show available dates for both king and twin', () => {
    expect(bookingComponent.availability).toEqual(AVAILABILITY.both);
  });

  it('Should show available dates for king', () => {
    bookingComponent.beKing();
    expect(bookingComponent.availability).toEqual(AVAILABILITY.king);
  });

  it('Should show available dates for twin', () => {
    bookingComponent.beTwin();
    expect(bookingComponent.availability).toEqual(AVAILABILITY.twin);
  });
});
