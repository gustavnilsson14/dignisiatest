// Link.react.test.js
import React from 'react';
import renderer from 'react-test-renderer';
import "regenerator-runtime/runtime";
import { UI, UIControls } from './components/UI.js';

test('Link changes the class when hovered', () => {
  const component = renderer.create(
    <UIControls startDate="2020-01-01" endDate="2021-02-01" />,
  );
  let tree = component.toJSON();
  expect(tree).toMatchSnapshot();
});
