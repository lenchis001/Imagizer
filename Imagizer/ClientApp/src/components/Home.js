import React, { Component } from 'react';
import { InputGroup, InputGroupAddon, InputGroupText, Input, Table, Button, Spinner, Row, Col } from 'reactstrap'
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props)

    this.state = {
      images: []
    }

    this.onSearchPatternChanges = this.onSearchPatternChanges.bind(this);

    this.patternSubject = new Subject();
  }

  componentDidMount() {
    this.getImageSubscription = this.patternSubject.pipe(debounceTime(1000)).subscribe(this.getImages)
  }

  componentWillUnmount() {
    this.getImageSubscription.unsubscribe()
  }

  async getImages(pattern) {
    const response = await fetch(`/api/v1/image?name=${pattern}`);

    if (response.ok) {

    } else {

    }
  }

  onSearchPatternChanges(e) {
    console.log(`Each: ${e.target.value}`);

    this.patternSubject.next(e.target.value);
  }

  render() {
    return (
      <div>
        <InputGroup>
          <InputGroupAddon addonType="prepend">
            <InputGroupText>Search</InputGroupText>
          </InputGroupAddon>
          <Input placeholder="type to search..." onChange={this.onSearchPatternChanges} />
          <InputGroupAddon addonType="append">
            <Button>Add image</Button>
          </InputGroupAddon>
        </InputGroup>

        {
          this.state.images.length > 0 ?
            <Table>
              <thead>
                <tr>
                  <th>#</th>
                  <th>Image</th>
                  <th>Name</th>
                  <th>Path</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <th scope="row">1</th>
                  <td><img height="128" src="https://cdn.jpegmini.com/user/images/slider_puffin_before_mobile.jpg" /></td>
                  <td>nfcoerjndlrtvbldtmklgmd</td>
                  <td>nfcoerjndlrtvbldtmklgmd.jpg</td>
                </tr>
              </tbody>
            </Table> :
            <Row>
              <Col xs='6' />
              <Col xs='auto'>
                <Spinner style={{ width: '3rem', height: '3rem' }} type="grow" />
              </Col>
              <Col xs='6' />
            </Row>
        }

      </div>
    );
  }
}
