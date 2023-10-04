import { useEffect, useState } from 'react';
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {v4 as uuid} from 'uuid';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);

  function handlerSelectActivity(id: string){
    setSelectedActivity(activities.find(x => x.id === id))
  }

  function handlerCancelSelectActivity(){
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string){
    id ? handlerSelectActivity(id) :handlerCancelSelectActivity;
    setEditMode(true);
  }

  function handleFormClose(){
    setEditMode(false);
  }

  function handleCreateOrEditActivity(activity: Activity) {
    activity.id 
      ? setActivities([...activities.filter(x => x.id !== activity.id), activity])
      : setActivities([...activities, {...activity, id: uuid()}]);
      setEditMode(false);
      setSelectedActivity(activity);
  }

  function handleDeleteActivity(id: string) {
    setActivities([...activities.filter(x => x.id !== id)])
  }

  useEffect(() => {
    axios.get<Activity[]>('https://localhost:7048/api/activities')
      .then(responce => {
        setActivities(responce.data)
      })
  }, [])

  return (
    <>
      <NavBar openForm={handleFormOpen}/>
      <Container style={{ marginTop: '7em' }}>
        <ActivityDashboard 
          activities={activities}
          selectedActivity = {selectedActivity}
          selectActivity = {handlerSelectActivity}
          cancelSelectActivity = {handlerCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={handleCreateOrEditActivity}
          deleteActivity={handleDeleteActivity}
          />
      </Container>
    </>
  )
}

export default App
