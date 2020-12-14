import { GetToken } from "../../services/JwtService"

export const getCommentByUrl = (url: string) =>{
    return fetch(url, {
        method: 'get',
        headers: {
            "Accept": "application/json",
            "Authorization": GetToken(),
        },

    }).then(response => response.json())
}