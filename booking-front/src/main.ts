import Vue from 'vue';
import Booking from './booking/Booking.vue';
import { BookingHttpRepository } from '@/booking/BookingHttpRepository';
import axios from 'axios';

Vue.config.productionTip = false;

const bookingRepository = () => new BookingHttpRepository(axios);

new Vue({
  render: (h) => h(Booking),
  provide: {
    bookingRepository,
  },
}).$mount('#app');
