import React, { Component, useEffect, useState } from 'react';
import { Container } from '@material-ui/core';
import { Header } from './Header/Header'
import { createMuiTheme, ThemeProvider, makeStyles, } from '@material-ui/core/styles';
import { Grid } from '@material-ui/core';
import { BottomNavigation } from '@material-ui/core';
import { useDispatch, useSelector } from 'react-redux';
import CombinedStore from '../store/CombinedStore';
import { fetchCurrentUser } from '../store/actions/_auth'
import Scrollbars from 'react-custom-scrollbars';
import { LoadingBox } from './Common/LoadingBox';

interface IProps {
  children: JSX.Element | JSX.Element[]
}
const Layout = (props: IProps) => {

    return (
      // <Grid container derection="column"  spacing={4}> вместо первого грида

      <div style={{ height: '100%', overflow: "hidden" }}>
        <Grid container spacing={4}>
          <Grid item xs={12} >
            <Header />
          </Grid>
          <Grid item xs={12}>
            <div style={{ position: 'absolute', top: '64px', bottom: '0px', right: '0px', left: '0px', marginTop: '0px' }}>
              <Scrollbars style={{ width: `100%`, height: '100%' }}  >
                <Container>
                  {props.children}
                </Container>
              </Scrollbars>
            </div>
          </Grid>
          <Grid item>
            <BottomNavigation>
            </BottomNavigation>
          </Grid>
        </Grid>

      </div>
    );

}
export default Layout
