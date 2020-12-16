import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";


export const removeFromFavorites = (postId: number) => {
    return fetch(baseUrl + `favorites`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            id: postId
        })

    });
}
export default removeFromFavorites;