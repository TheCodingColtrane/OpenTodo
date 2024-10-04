import { Category, Task } from "../../../../types";

export type TaskListProps = {
    tasks: Task[];
    onCompleteTask: (taskId: string) => void;
  };

  
export type TaskItemProps = {
    task: Task;
    onComplete?: (taskCode: string) => void;
  };

  export type NewCategoryResponse = {
    success: boolean;
    code: string
  }

  export interface TaskModalProps {
    open: boolean;
    handleClose: () => void;
    type: number;
    task?: Task;
  }

  export interface CategoriesModalProps {
    open: boolean;
    handleClose: () => void;
    handleCategories: (category: Category) => void
  }