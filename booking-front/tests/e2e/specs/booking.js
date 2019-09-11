const TITLE = '[data-selector=title]';
const TWIN = '[data-selector=twin]';
const KING = '[data-selector=king]';
const START_DATE = '[data-selector=start-date]';
const END_DATE = '[data-selector=end-date]';
const PAY = '[data-selector=pay]';

describe('Booking', () => {
  beforeEach(() => {
    cy.server();
    cy.route('POST', '/api/booking/book-room', {}).as('pay');
    cy.visit('/');
  });

  it('Booking URL', () => {
    cy.contains(TITLE, 'Your reservation');
    cy.get(TWIN);
    cy.get(KING);
    cy.get(START_DATE);
    cy.get(END_DATE);
    cy.contains(PAY, 'Pay $300');
  });

  it('Should pay', () => {
    cy.get(KING).click();
    cy.get(START_DATE).type('2019-09-10');
    cy.get(END_DATE).type('2019-09-11');
    cy.get(PAY).click();
    cy.wait('@pay').then((xhr) => {
      expect(xhr.status).to.eq(200);
      expect(xhr.request.body).to.eql({
        amount: 300,
        guestId: '123',
        roomType: 'king',
        startDate: '2019-09-10',
        endDate: '2019-09-11',
      });
    });
  });
});
