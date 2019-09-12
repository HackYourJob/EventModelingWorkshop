const TITLE = '[data-selector=title]';
const TWIN = '[data-selector=twin]';
const KING = '[data-selector=king]';
const START_DATE = '[data-selector=start-date]';
const END_DATE = '[data-selector=end-date]';
const PAY = '[data-selector=pay]';
const AVAILABILITY_ITEM = '[data-selector=availability-item]';

describe('Booking', () => {
  beforeEach(() => {
    cy.server();
    cy.route('POST', '/api/booking/book-room', {}).as('pay');
    cy.route('GET', '/api/availability', {
      king: [
        '2019-09-08',
        '2019-09-10',
        '2019-09-11',
      ],
      twin: [
        '2019-09-09',
        '2019-09-10',
        '2019-09-11',
        '2019-09-12',
      ]
    }).as('availability');
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

  it('Should show availability', () => {
    cy.get(AVAILABILITY_ITEM).should('have.length', 2);
    cy.contains(AVAILABILITY_ITEM, '2019-09-10');
    cy.contains(AVAILABILITY_ITEM, '2019-09-11');
  });

  it('Should show availability for king', () => {
    cy.get(KING).click();
    cy.get(AVAILABILITY_ITEM).should('have.length', 3);
  });

  it('Should show availability for twin', () => {
    cy.get(TWIN).click();
    cy.get(AVAILABILITY_ITEM).should('have.length', 4);
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
