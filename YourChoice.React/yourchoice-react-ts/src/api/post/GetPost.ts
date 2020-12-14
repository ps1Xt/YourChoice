import { GetToken } from "../../services/JwtService";
import { baseUrl } from "../baseUrl";
import { PostForView } from "./Models/PostForView";

export const GetPost = (id: number): Promise<PostForView> => {
    return fetch(baseUrl + `post/${id}`, {
        method: 'get',
        headers: {
            "Accept": "application/json",
            Authorization: GetToken()

        }
    }).then(res => res.json()).then(res => res);
}