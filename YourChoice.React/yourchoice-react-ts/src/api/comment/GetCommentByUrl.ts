import { GetToken } from "../../helpers/JwtService"
import { PostComment } from "../post/Models/PostComment";

export const getCommentByUrl = async (url: string) => {
    let response = await fetch(url, {
        method: 'get',
        headers: {
            "Accept": "application/json",
            "Authorization": GetToken(),
        },

    })

    if (!response.ok) {
        throw Error("Failed to get a comment")
    }

    let result: PostComment = await response.json();

    return result;


}