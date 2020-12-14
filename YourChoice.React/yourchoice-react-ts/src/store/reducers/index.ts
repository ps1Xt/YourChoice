import {combineReducers} from 'redux';
import auth from './auth';
import createPost from './createPost'
import setFiles from './setFiles'
export default combineReducers({
 auth : auth,
 createPost : createPost,
 setFiles: setFiles,
});
