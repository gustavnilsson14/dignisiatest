import React, { Component } from 'react';
//﻿import { InputGroup, Dropdown, FormControl,DropdownToggle,DropdownMenu,DropdownItem } from 'reactstrap';
import { Button, Form, FormGroup, Label, Input, FormText, Dropdown, DropdownToggle, DropdownMenu, DropdownItem, Container, Row, Col } from 'reactstrap';
import {MockData} from '../mock/MockData.js'
const ReactHighcharts = require('react-highcharts');

class UI extends React.Component {
  constructor(props) {
    super(props);
    this.state = { segment: null, segmentId: 1, caseAgeRangeEnd: 1 };
  }
	onFormChange = (newState) => {
		this.setState(newState);
	}
	render() {
		return (
			<div>
				<UIControls startDate="2020-01-01" endDate="2021-02-01" onFormChange={this.onFormChange.bind(this)} />
				<Container>
				  <Row>
				    <Col>
							<DebtSizeWidget key={this.state.segmentId + this.state.caseAgeRangeEnd} {...this.state}/>
						</Col>
				    <Col>
							<CaseOverviewWidget key={this.state.segmentId + this.state.caseAgeRangeEnd} {...this.state}/>
						</Col>
				  </Row>
				</Container>
			</div>
		);
	}
}
class UIControls extends React.Component {
  //const [dropdownOpen, setDropdownOpen] = useState(false);

  constructor(props) {
    super(props);
    this.state = { segment: null, segmentId: 1, caseAgeRangeEnd: 0, loading: true };
  }
  componentDidMount = () => {
    this.getSegments();
  }
  toggle = () => {
		this.setState({isOpen: !this.isOpen()});
	}
	isOpen = () => {
		if (!this.state){
			return false;
		}
		return this.state.isOpen;
	}
	getSegmentOptionsRecursive = (segment, parentSegment = null) => {
		if (segment == null)
			return null;
		var result = [this.getSegmentOption(segment, parentSegment)];
		segment.children.forEach((child, i) => {
			result = [result, ...this.getSegmentOptionsRecursive(child, segment)];
		});
		return result;
	}
	getSegmentOption = (segment, parentSegment) => {
		var segmentDisplayName = segment.segmentName;
		if (parentSegment != null)
			segmentDisplayName = `${parentSegment.segmentName} - ${segment.segmentName}`;
		return(
			<option key={segment.segmentId} value={segment.segmentId}>{segmentDisplayName}</option>
		);
	}
	getCaseAgeRange = () => {
		var difference = this.getMonthDifference(new Date(this.props.startDate), new Date(this.props.endDate))
		return difference;
	}
	getCaseAgeRangeEndDate = () => {
		var endDate = new Date(this.props.startDate);
		endDate.setMonth(endDate.getMonth() + this.state.caseAgeRangeEnd);
		return endDate.toISOString().split('T')[0];
	}
	onCaseAgeRangeChange = (event) => {
		var newState = {caseAgeRangeEnd: parseInt(event.target.value)};
		this.props.onFormChange(newState);
		this.setState(newState);
	}
	onSegmentSelectChange = (event) => {
		var newState = {segmentId: event.target.value};
		this.props.onFormChange(newState);
		this.setState(newState);
	}
	getMonthDifference = (d1, d2) => {
		return d2.getMonth() - d1.getMonth() +
   (12 * (d2.getFullYear() - d1.getFullYear()))
	}
	async getSegments(){
	    const response = await fetch('segments');
	    const data = await response.json();
			this.setState({ segment: data });
	}
	render() {
		return (
			<Form>
	      <FormGroup>
	        <Label for="segmentSelect">Segment</Label>
	        <Input value={this.state.segmentId} onChange={this.onSegmentSelectChange} type="select" name="segmentSelect" id="segmentSelect">
						{this.getSegmentOptionsRecursive(this.state.segment)}
	        </Input>
	      </FormGroup>
		      <FormGroup>
		        <Label for="caseAgeRange">Case Age</Label>
						<span style={{float:"right"}}>From {this.props.startDate} to {this.getCaseAgeRangeEndDate()}</span>
						<Input onChange={this.onCaseAgeRangeChange} type="range" value={this.state.caseAgeRangeEnd} min="0" max={this.getCaseAgeRange()} step="1" name="caseAgeRange" id="caseAgeRange"/>
						<span>{this.props.startDate}</span>
						<span style={{float:"right"}}>{this.props.endDate}</span>
		      </FormGroup>
	    </Form>
		);
	}
}
class DebtSizeWidget extends React.Component {
  constructor(props) {
    super(props);
    this.state = { chartConfig: [], loading: true };
  }
	componentDidMount = () => {
		this.getChartConfig();
	}
	async getChartConfig(){
		var api = `chart-data/1?segmentId=${this.props.segmentId}&caseAge=${this.props.caseAgeRangeEnd}`;
		const response = await fetch(api);
    const data = await response.json();
		this.setState({ chartConfig: MockData.debtWidgetMockData(data) });
	}
	render() {
		return (
			<ReactHighcharts config={this.state.chartConfig}></ReactHighcharts>
		);
	}
}
class CaseOverviewWidget extends React.Component {
  constructor(props) {
    super(props);
    this.state = { chartConfig: [], loading: true };
  }
	componentDidMount = () => {
		this.getChartConfig();
	}
	async getChartConfig(){
		var api = `chart-data/2?segmentId=${this.props.segmentId}&caseAge=${this.props.caseAgeRangeEnd}`;
    const response = await fetch(api);
    const data = await response.json();
		this.setState({ chartConfig: MockData.caseOverviewMockData(data) });
	}
	render() {
		return (
			<ReactHighcharts config={this.state.chartConfig}></ReactHighcharts>
		);
	}
}

export { UI, UIControls };
