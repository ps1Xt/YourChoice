import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import InputBase from '@material-ui/core/InputBase';
import IconButton from '@material-ui/core/IconButton';
import SearchIcon from '@material-ui/icons/Search';
import { useForm } from 'react-hook-form';
import { MainPageRequest } from '../../api/post/Models/MainPageRequest';
import { Filter } from '../../api/grid/models/Filter';
import { pageRequestState } from './PostNavMenu';
const useStyles = makeStyles((theme) => ({
  root: {
    padding: '2px 4px',
    display: 'flex',
    alignItems: 'center',
    width: 400,
    border: '1px solid #e0e0e0'
  },
  input: {
    marginLeft: theme.spacing(1),
    flex: 1,
  },
  iconButton: {
    padding: 10,
  },
  divider: {
    height: 28,
    margin: 4,
  },
}));

export default function Search(props: pageRequestState) {
  const classes = useStyles();
  const [pageRequest, setPageRequest] = props.pageRequest;
  const { register, handleSubmit, errors } = useForm();

  const filterHandler = (data: any) => {
    var value = data.searchInput
    let filter: Filter = {
      path: 'title',
      value: value,
      operation: 'contains'
    }
    setPageRequest({
      ...pageRequest,
      requestFilters: {
        ...pageRequest.requestFilters,
        filters: [filter]
      }
    })
    
  }

  return (
    <form  onSubmit={handleSubmit(filterHandler)}>
      <Paper className={classes.root} elevation={0}>
        <InputBase
          className={classes.input}
          placeholder="Search posts"
          inputProps={{ 'aria-label': 'Search posts' }}
          name="searchInput"
          inputRef={register}
        />
        <IconButton type="submit" className={classes.iconButton} aria-label="search">
          <SearchIcon />
        </IconButton>
      </Paper>
    </form >
  );
}
