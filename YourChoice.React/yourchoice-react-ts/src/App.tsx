import React, { useEffect } from 'react';
import { Route } from "react-router";
import { Main } from './components/Main/Main';
import Layout from './components/Layout';
import NavTheme from './Themes/NavTheme'
import { ThemeProvider } from '@material-ui/core';
import { SignIn } from './components/SignIn/Login';
import CreatePost from './components/PostCreator/CreatePost'
import SignUp from './components/SignUp/SignUp'
import { BrowserRouter } from 'react-router-dom';
import { Post } from './components/Post/Post';
import { DataGrid } from './components/Grid/Grid';
import { useDispatch, useSelector } from 'react-redux';
import CombinedStore from './store/CombinedStore';
import { fetchCurrentUser } from './store/actions/_auth';
import { LoadingBox } from './components/Common/LoadingBox';
function App() {
  const dispatch = useDispatch();

  const isAuthenticated = useSelector<CombinedStore, boolean>(
    (s) => s.auth.isAuthenticated
  );

  const isFetchingCurrentUser = useSelector<CombinedStore, boolean>(
    (s) => s.auth.isFetchingCurrentUser
  );

  useEffect(() => {
    dispatch(fetchCurrentUser());
  }, [dispatch]);

  if (isFetchingCurrentUser) {
    return (<LoadingBox></LoadingBox>)
  }
  else

  return (
    <BrowserRouter>
      <ThemeProvider theme={NavTheme}>
        <Layout >
          <Route exact path="/" component={Main} />
          <Route exact path='/SignIn' component={SignIn} />
          <Route exact path='/CreatePost' component={CreatePost} />
          <Route exact path='/SignUp' component={SignUp} />
          <Route exact path='/Grid' component={DataGrid} />
          <Route path="/post/:postId" component={Post} />
        </Layout>
      </ThemeProvider>
    </BrowserRouter>

  );
}

export default App;
