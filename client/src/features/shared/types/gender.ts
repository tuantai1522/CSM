export const GenderType = {
  Male: 1,
  Female: 2,
  Other: 3,
} as const;

export type GenderType = (typeof GenderType)[keyof typeof GenderType];
