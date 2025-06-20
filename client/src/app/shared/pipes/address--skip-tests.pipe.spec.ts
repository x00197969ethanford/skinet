import { AddressSkipTestsPipe } from './address--skip-tests.pipe';

describe('AddressSkipTestsPipe', () => {
  it('create an instance', () => {
    const pipe = new AddressSkipTestsPipe();
    expect(pipe).toBeTruthy();
  });
});
