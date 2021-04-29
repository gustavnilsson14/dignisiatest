import React, { Component } from 'react';
import { Route } from 'react-router';
import { UI } from './components/UI';
import { Container } from 'reactstrap';
import './custom.css';
const ReactHighcharts = require('react-highcharts');

export default class App extends Component {
  static displayName = App.name;
   render() {
    return (
      <Container fluid="md">
        <img className="bg-image" src="Untitled.png" />
        <UI />
      </Container>
    );
  }
}
