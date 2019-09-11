import { shallowMount, Wrapper } from '@vue/test-utils';
import Booking from '@/booking/Booking.vue';
import BookingComponent from '@/booking/Booking.component.ts';
import { BookingHttpRepository } from '@/booking/BookingHttpRepository';
import { SinonStubbedInstance } from 'sinon';
import sinon from 'sinon';

let wrapper: Wrapper<BookingComponent>;
let bookingComponent: BookingComponent;
let stubbedBookingHttpRepository: SinonStubbedInstance<BookingHttpRepository>;

describe('Booking', () => {
  it('Should be a vue instance', () => {
    stubbedBookingHttpRepository = sinon.createStubInstance(BookingHttpRepository);
    wrapper = shallowMount<BookingComponent>(Booking, {
      provide: {
        bookingRepository: () => stubbedBookingHttpRepository,
      },
    });
    bookingComponent = wrapper.vm;
    expect(wrapper.isVueInstance()).toBeTruthy();
  });

  it('Should pay', () => {
    bookingComponent.amount = '300';
    bookingComponent.startDate = '2019-09-10';
    bookingComponent.endDate = '2019-09-11';
    bookingComponent.roomType = 'king';
    bookingComponent.pay();

    expect(stubbedBookingHttpRepository.pay.getCall(0).args).toEqual([
      300,
      '2019-09-10',
      '2019-09-11',
      'king',
    ]);
  });
});
