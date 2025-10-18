import { AggregateRoot } from 'src/libs/ddd/aggregate-root.base';

export class CountryEntity extends AggregateRoot {
  private readonly id: number;
  private name: string;

  private constructor() {
    super();
  }

  static create(name: string): CountryEntity {
    var country = new CountryEntity();
    country.name = name;

    return country;
  }

  get Id() {
    return this.id;
  }

  get Name() {
    return this.name;
  }

  changeName(newName: string) {
    this.name = newName;
  }
}
