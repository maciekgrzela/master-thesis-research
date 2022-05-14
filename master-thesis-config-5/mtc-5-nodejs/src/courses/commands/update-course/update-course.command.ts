import { UpdateCourseDto } from 'src/courses/dtos/updateCourseDto';

export class UpdateCourseCommand {
  constructor(public id: string, public payload: UpdateCourseDto) {}
}
