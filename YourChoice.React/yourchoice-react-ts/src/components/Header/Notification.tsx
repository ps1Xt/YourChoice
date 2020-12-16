import React, { useContext, useEffect, useState } from 'react';
import ClickAwayListener from '@material-ui/core/ClickAwayListener';
import Grow from '@material-ui/core/Grow';
import Paper from '@material-ui/core/Paper';
import Popper from '@material-ui/core/Popper';
import MenuList from '@material-ui/core/MenuList';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import { Badge, Box, IconButton, List, ListItem, ListItemText, ListSubheader } from '@material-ui/core';
import NotificationsIcon from '@material-ui/icons/Notifications';
import Scrollbars from 'react-custom-scrollbars';
import CircularProgress from '@material-ui/core/CircularProgress';
import { Message } from '../../api/notification/Models/Message';
import { getCountOfNewMessages } from '../../api/notification/GetCountOfNewMessages';
import { getMessages } from '../../api/notification/GetMessages';
import { ErrorBox } from '../Common/ErrorBox';
import { HubConnectionBuilder } from '@microsoft/signalr'
import * as signalR from "@microsoft/signalr";
import { GetToken } from '../../helpers/JwtService';
import { getConnection } from '../../api/notification/GetConnection'
import { useDispatch, useSelector } from 'react-redux';
import CombinedStore from '../../store/CombinedStore';
import { SignalRContext } from '../../Context/SignalRContext'
import { readMessages, setNewMessages } from '../../store/actions/_notification';
import {readMessages as readMessagesApi} from '../../api/notification/ReadMessages'
const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex'
    },
    text: {
      padding: theme.spacing(2, 2, 0),
    },
    paper: {
      width: '400px',
      height: '300px',
    },
    list: {
      marginBottom: theme.spacing(2),
    },
    subheader: {
      backgroundColor: theme.palette.background.paper,

    },
    appBar: {
      top: 'auto',
      bottom: 0,
    },
    popper: {
      position: 'absolute',
      zIndex: 100
    },
    grow: {
      flexGrow: 1,
    },
    fabButton: {
      position: 'absolute',
      zIndex: 1,
      top: -30,
      left: 0,
      right: 0,
      margin: '0 auto',
    },
  }),
);


export default function Notification() {
  const classes = useStyles();
  const [open, setOpen] = React.useState(false);
  const [messages, setMessages] = useState(new Array<Message>())
  const anchorRef = React.useRef<HTMLButtonElement>(null);
  const [isLoading, setIsoLoading] = useState(true);
  const [error, setError] = useState(false)
  const dispatch = useDispatch();
  let newMessages = useSelector<CombinedStore, number>(
    (s) => s.notification.number
  )

  const handleToggle = async () => {
    setOpen((prevOpen) => !prevOpen);
    await messagesHandler()
    await readMessagesHandler()

  };

  const handleClose = (event: React.MouseEvent<EventTarget>) => {
    if (anchorRef.current && anchorRef.current.contains(event.target as HTMLElement)) {
      return;
    }

    setOpen(false);
  };

  const messagesHandler = async () => {
    try {
      setError(false)
      setIsoLoading(true)
      let messages = await getMessages()
      setMessages(messages)
    }
    catch {
      setError(true)
    }
    finally {
      setIsoLoading(false)
    }
  }

  const readMessagesHandler = async () => {
    await readMessagesApi();
    dispatch(readMessages());
  }
  const getNumberOfNewMessages = async () => {
    try{
      let number = await getCountOfNewMessages()
      dispatch(setNewMessages({ number: number.number }))
    }
    catch{}
    
  }
  useEffect(() => {
    getNumberOfNewMessages()
  }, [])

  const messageList = () => {
    if (isLoading)
      return (
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          minHeight="100%"
        >
          <CircularProgress />
        </Box>
      )
    else if (error) {
      return (<ErrorBox message="Failed to load messages" color="error" />)
    }
    else {
      return (
        <List className={classes.list}>
          {messages.map(({ id, title, text, date }) => (
            <div key={id}>
              <ListItem button>
                <ListItemText primary={title} secondary={text} />
              </ListItem>
            </div>
          ))}
        </List>
      )
    }
  }

  const prevOpen = React.useRef(open);
  React.useEffect(() => {
    if (prevOpen.current === true && open === false) {
      anchorRef.current!.focus();
    }

    prevOpen.current = open;
  }, [open]);

  return (
    <div className={classes.root}>
      <IconButton ref={anchorRef}
        aria-controls={open ? 'menu-list-grow' : undefined}
        aria-haspopup="true"
        onClick={handleToggle}>
        <Badge badgeContent={newMessages} color="secondary">
          <NotificationsIcon color="primary"></NotificationsIcon>
        </Badge >
      </IconButton>
      <Popper open={open} anchorEl={anchorRef.current} role={undefined} className={classes.popper} transition>
        {({ TransitionProps, placement }) => (
          <Grow
            {...TransitionProps}
            style={{ transformOrigin: placement === 'bottom' ? 'center top' : 'center bottom' }}
          >
            <Paper >
              <ClickAwayListener onClickAway={handleClose}>
                <MenuList autoFocusItem={open} id="menu-list-grow" >

                  <div>
                    <Paper className={classes.paper} elevation={0}>

                      <Scrollbars style={{ height: '300px' }}>

                        {messageList()}

                      </Scrollbars>

                    </Paper>
                  </div>
                </MenuList>
              </ClickAwayListener>
            </Paper>
          </Grow>
        )}
      </Popper>
    </div >
  )
}
