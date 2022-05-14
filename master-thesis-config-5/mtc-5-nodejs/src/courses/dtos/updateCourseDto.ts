export type UpdateCourseDto = {
  name: string;
  grossPrice: number;
  netPrice: number;
  tax: number;
  preparationTimeInMinutes: number;
  coursesCategoryId: string;
  ingredients: SaveIngredient[];
};

export type SaveIngredient = {
  productId: string;
  amount: number;
};
