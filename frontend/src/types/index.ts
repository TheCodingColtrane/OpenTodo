export type Task = {
    code: string,
    title: string,
    description: string,
    isCompleted: boolean,
    category: number,
    createdAt: Date,
    updatedAt: Date,
    userId: number;
    dueDate: string | Date;
    assignedUser?: User
}

export type User = {
    code: string,
    firstName: string,
    lastName: string,
    dob: Date,
    dob2?: string
    username: string,
    passwordHash: string,
    createdAt: Date
}


export type Category = {
    code: string;
    name: string;
    value: number;
    createdAt: Date;
    updatedAt?: Date;
    boardCode: string;
}

export type Board = {
    code: string;
    name: string;
    createdAt: Date;
    updatedAt?: Date;
    userId: string;
}