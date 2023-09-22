import './App.css';
import TextField from "@mui/material/TextField";
import { BiUserVoice } from 'react-icons/bi'

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <div style={{display: 'flex', flexDirection:'row'}}>
          <div style={{paddingRight:20}}>
            <TextField
              id="outlined-basic"
              variant="outlined"
              fullWidth
              label="Search"
              style={{color: '#fff', width: '100%'}}
            />
        </div>
        <div style={{alignSelf: 'center'}}>
        <BiUserVoice size={40}/>
        </div>
        </div>
        <div>Happy Learning</div>
      </header>
    </div>
  );
}

export default App;
