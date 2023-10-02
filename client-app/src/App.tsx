import { useEffect, useState } from 'react'
import './App.css'
import axios from 'axios'
import { Header, List } from 'semantic-ui-react';

function App() {
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7048/api/activities')
      .then(responce => {
        setActivities(responce.data)
      })
  }, [])

  return (
    <div>
      <Header as='h2' icon='users' content='Activities' />
      <List>
        {activities.map((activity: any) => (
          <List.Item key={activity.id}>
            {activity.title}
          </List.Item>
        ))}
      </List>
    </div>
  )
}

export default App
