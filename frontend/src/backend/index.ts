// import { Category } from "../types";

const baseUrl = "https://localhost:7167"
const endpoints = {
    users: baseUrl + "/user",
    tasks: baseUrl + "/task",
    auth: baseUrl + "/auth",
    categories: baseUrl + "/category",
    boards: baseUrl + "/board"
};

export const userEndpoints = {
    base: endpoints.users,
    getById: (id: string) => userEndpoints.base + "/" + id,
    getUsernameAvailability: (username: string) => endpoints.users + "/username/" + username,
    SearchUserByTerm: (term: string) =>  endpoints.users + "/search/" + term
}

export const authEndpoints = {
    base: endpoints.auth
}

export const tasksEndpoints = {
    base: endpoints.tasks,
    getById: (code: string) => tasksEndpoints.base + "/" + code,
    getByUser: (code: string) => tasksEndpoints.base + "/" + code + "/user/" ,
    getTasksByCategory: (boardCode: string, categoryCode: string, pageCode: string) => {
        const base = tasksEndpoints.base + "/category/" + categoryCode + "/" + boardCode
        if(pageCode){
            return base + "/" + pageCode
        }
        return base + "/"

    }
}

export const categoriesEndpoints = {
    base: endpoints.categories,
    getById: (code: string) => categoriesEndpoints.base + "/" + code,
    getBoardCategories: (code: string) => categoriesEndpoints.base + "/board/" + code,
    
}

export const boardsEndpoints = {
    base: endpoints.boards,
    getById: (code: string) => boardsEndpoints.base + "/" + code,
    getUserBoards: () => boardsEndpoints.base + "/user-board",
    getTasksByBoard: (code: string) => boardsEndpoints.base + "/" + code + "/task",
    searchTasksOnBoard: (code: string, term: string) =>  boardsEndpoints.base + "/" + code + "/task/search/" + term
}


export const backend = {
    get: async<T>(uri: string): Promise<T | null>  => {
        try {
            const req = await fetch(uri, {
                credentials: 'include'
            })
            const data = req.json() as T;
            return data;
    
        } catch (error) {
            console.log(error)
            return null
        }
     },
     post: async<T>(uri: string, data: T): Promise<Response | null>  => {
        try {
            const req = await fetch(uri, {
                method: 'POST',
                credentials: 'include',
                body: JSON.stringify(data),
                headers: { "Content-Type": "application/json"},
            });
    
            return req;
    
        } catch (error) {
            console.log(error)
            return null
        }

},
put: async<T>(uri: string, data: T): Promise<Response | null>  => {
    try {
        const req = await fetch(uri, {
            method: 'PUT',
            credentials: 'include',
            body: JSON.stringify(data),
            headers: { "Content-Type": "application/json"},
        });
 
        return req;
 
    } catch (error) {
        console.log(error)
        return null
    }
 
 },

 delete: async<T>(uri: string, data?: T): Promise<Response | null>  => {
    try {
        const req = await fetch(uri, {
            method: 'DELETE',
            credentials: 'include',
            body: data ? JSON.stringify(data) : null,
            headers: { "Content-Type": "application/json"},
        });
 
        return req;
 
    } catch (error) {
        console.log(error)
        return null
    }
 
 },
 


}

 