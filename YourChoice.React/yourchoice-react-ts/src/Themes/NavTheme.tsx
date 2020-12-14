import { createMuiTheme, ThemeProvider, makeStyles, } from '@material-ui/core/styles';
import red from '@material-ui/core/colors/red';
import blue from '@material-ui/core/colors/blue';
const theme = createMuiTheme({
    palette: {
        primary: blue,
        secondary: red
    },
    typography: {
        h4: {
            fontWeight: 700
        },
        button: {
            textTransform: 'none'
          }

    }
});
export default theme;