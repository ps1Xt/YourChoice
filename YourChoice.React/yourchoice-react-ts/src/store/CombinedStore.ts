import { AuthStore } from "./reducers/auth";
import { CreatePostStore } from "./reducers/createPost";
import { numberOfNewMessages } from "./reducers/notification";

export default interface CombinedStore {
  auth: AuthStore,
  createPost: CreatePostStore,
  notification: numberOfNewMessages
}