import React, { createContext, useEffect, useState } from 'react';
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
import { HubConnectionBuilder } from '@microsoft/signalr';
import * as signalR from "@microsoft/signalr";
import { GetToken } from './helpers/JwtService';
import { newMessage } from './store/actions/_notification';
import {SignalRContext} from './Context/SignalRContext'



function App() {
  const dispatch = useDispatch();
  const [connection, setConnection] = useState<any>()
  const isAuthenticated = useSelector<CombinedStore, boolean>(
    (s) => s.auth.isAuthenticated
  );

  const isFetchingCurrentUser = useSelector<CombinedStore, boolean>(
    (s) => s.auth.isFetchingCurrentUser
  );

  useEffect(() => {
    dispatch(fetchCurrentUser());
  }, [dispatch]);

  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/hubs/notify", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => GetToken().split('Bearer ')[1]
      })
      .withAutomaticReconnect()
      .build()

    connection.start().then((e) => {
      console.log("started")
    })
      .catch(err => {
        console.log('connection error');
      })

    connection.on('NewMessage', () => {
      {
        console.log("good")
        dispatch(newMessage())
      }
    })
    setConnection(connection)
  }, [])


  if (isFetchingCurrentUser) {
    return (<LoadingBox></LoadingBox>)
  }
  else

    return (
      <BrowserRouter>
        <ThemeProvider theme={NavTheme}>
          <SignalRContext.Provider value={{
            FavoritesNotify(postId: number) {
              connection.invoke("FavoritesNotify", postId)
                .then(x => { })
                .catch(x => { })
            },
            SubscriptionNotify(postId: number) {
              connection.invoke("SubscriptionNotify", postId)
                .then(x => { })
                .catch(x => { })
            },
            SubscribersNotify() {
              connection.invoke("SubscribersNotify")
                .then(x => { })
                .catch(x => { })
            },
            CommentNotify(postId: number) {
              connection.invoke("CommentNotify", postId)
                .then(x => { })
                .catch(x => { })
            },
            RatingNotify(postId: number) {
              connection.invoke("RatingNotify", postId)
                .then(x => { })
                .catch(x => { })
            },  connection
          }}>
            <Layout >
              <Route exact path="/" component={Main} />
              <Route exact path='/SignIn' component={SignIn} />
              <Route exact path='/CreatePost' component={CreatePost} />
              <Route exact path='/SignUp' component={SignUp} />
              <Route exact path='/Grid' component={DataGrid} />
              <Route path="/post/:postId" component={Post} />
            </Layout>
          </SignalRContext.Provider>
        </ThemeProvider>
      </BrowserRouter >

    );
}

export default App;
