export class Availability {
  constructor(private kingDates: string[], private twinDates: string[]) {
  }

  public get king(): string[] {
    return this.kingDates;
  }

  public get twin(): string[] {
    return this.twinDates;
  }

  public get both(): string[] {
    return this.kingDates.filter((date) => this.twinDates.includes(date));
  }
}
