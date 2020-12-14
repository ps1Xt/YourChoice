import { PostComment } from "./PostComment";
import { PostPartForView } from "./PostPartForView";

export interface PostForView {
    id: number;
    title: string;
    userName: string;
    userId: number;
    description: string;
    postParts: PostPartForView[];
    date: string;
    comments: PostComment[];
    isInFavorites: boolean;
    isSubscribed: boolean;
    avgRating: number;
    size: number;
}