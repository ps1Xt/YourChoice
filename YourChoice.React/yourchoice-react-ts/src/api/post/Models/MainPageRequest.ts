import { PageRequest } from "../../grid/models/PageRequest";

export interface MainPageRequest extends PageRequest {

    section: "Home" | "Subscriptions" | "Favorites" | "MyPosts"
}