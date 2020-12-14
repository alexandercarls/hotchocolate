import React, { FunctionComponent, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import { State } from "../../state";
import { toggleAside } from "../../state/common";
import { BodyStyle, DocPageStickySideBarStyle} from "./doc-page-elements";
import { DocPagePaneHeader } from "./doc-page-pane-header";
import styled from 'styled-components';
import {BoxShadow, IsMobile, IsSmallDesktop, SmallDesktopBreakpointNumber} from './shared-style';

export const DocPageAside: FunctionComponent = ({ children }) => {
  const showAside = useSelector<State, boolean>(
    (state) => state.common.showAside
  );
  const dispatch = useDispatch();

  const handleCloseAside = useCallback(() => {
    dispatch(toggleAside());
  }, []);

  return (
    <Aside className={showAside ? "show" : ""}>
      <BodyStyle disableScrolling={showAside} />
        <DocPagePaneHeader
          title="About this article"
          showWhenScreenWidthIsSmallerThan={SmallDesktopBreakpointNumber}
          onClose={handleCloseAside}
        />
        {children}
    </Aside>
  );
};

export const Aside = styled.aside`
  ${DocPageStickySideBarStyle}

  margin-left: 0;
  transition: transform 250ms;
  background-color: white;

  padding: 25px 0 0;

  &.show {
    transform: none;
  }

  ${IsSmallDesktop(`
    transform: translateX(100%);
    ${BoxShadow}
  `)}

  ${IsMobile(`
    width: 100%;
  `)}
`;
