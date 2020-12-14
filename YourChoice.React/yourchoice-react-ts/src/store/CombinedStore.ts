import { AuthStore } from "./reducers/auth";
import { CreatePostStore } from "./reducers/createPost";
import { setfiles } from "./reducers/setFiles";

export default interface CombinedStore {
  auth: AuthStore,
  createPost: CreatePostStore,
  setFiles: setfiles,
}