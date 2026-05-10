test('should fail for empty property title', () => {
  const value = "";
  const result = value.trim() !== "";
  expect(result).toBe(false);
});

test('should pass for valid property title', () => {
  const value = "Apartment in Amman";
  const result = value.trim() !== "";
  expect(result).toBe(true);
});
