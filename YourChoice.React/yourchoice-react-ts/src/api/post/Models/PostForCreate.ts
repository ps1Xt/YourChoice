import { PostPartsForCreate } from "./PostPartsForCreate";

export interface PostForCreate {
    description: string,
    size: number,
    postParts: PostPartsForCreate[]
}